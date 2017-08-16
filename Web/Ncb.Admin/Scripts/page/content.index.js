(function (angular, doc) {
    'use strict'

    angular.module('contentApp', ['ui.bootstrap', 'ngSanitize']).controller('ContentCtrl', function ($scope, $http, $sce) {
        $scope.pageSize = 20;
        $scope.totalItems = 0;
        $scope.currentPage = 1;
        $scope.content = {};
        $scope.detailHtml = '';
        $scope.getList = function (e) {
            var btn;
            if (e) {
                btn = $(e.target);
                btn.button('loading');
            }

            $http({
                method: 'POST',
                url: '/content/GetList',
                headers: { 'Content-Type': undefined },
                transformRequest: function (data) {
                    data = new FormData(doc.getElementById('queryForm'));
                    data.append('pageIndex', $scope.currentPage);
                    data.append('pageSize', $scope.pageSize);
                    return data;
                }
            }).success(function (resp) {
                if (resp.Success) {
                    resp.Data.length === 0 && common.alert('未查到数据！');
                    $scope.List = resp.Data;
                    $scope.totalItems = resp.TotalCount;
                } else {
                    common.alert(resp.Message || '获取数据失败！');
                }
                btn && btn.button('reset');
            }).error(function () {
                common.alert('出现错误或者网络异常');
                $scope.totalItems = 0;
                btn && btn.button('reset');
            });
        };
        $scope.viewDetail = function (id, e) {
            var btn = $(e.target);
            btn.button('loading');
            $http.get('/content/getContent?id=' + id).success(function (resp) {
                if (resp.indexOf('>') > 0) {
                    $scope.detailHtml = $sce.trustAsHtml(resp);
                    ui.detailModel.modal('show');
                } else {
                    common.alert(resp);
                }
                btn.button('reset');
            }).error(function () {
                common.alert('出现错误或者网络异常');
                btn.button('reset');
            });
        };

        var ui = {
            detailModel: $('#detailModal'),
        };

        ui.detailModel.on('hidden.bs.modal', function () {
            $scope.detailHtml = null;
        });

        $scope.getList();

        $scope.delete = function (id, e) {
            var target = $(e.target);

            if (!target.data('delete')) {
                target.text('确定要删除吗');
                target.data('delete', true).css({ 'color': 'red' });
                return;
            }

            common.alert("正在删除...", function () {
                $http.post('/content/delete?id=' + id + '&t=' + common.timestamp())
                    .success(function (resp) {
                        if (resp.Success) {
                            common.alert('删除成功！');
                            $scope.getList();
                        } else {
                            common.alert(resp.Message);
                            target.text('删除');
                            target.data('delete', false).css({ 'color': '' });
                        }
                    }).error(function (resp) {
                        common.alert('出现错误或者网络异常！');
                        $scope.List = [];
                        $scope.totalItems = 0;
                    });
            });
        };
    });
})(window.angular, document);
