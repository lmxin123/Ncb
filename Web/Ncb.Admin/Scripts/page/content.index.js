(function (angular, doc) {
    'use strict'

    angular.module('contentApp', ['ui.bootstrap', 'ngSanitize', 'common']).controller('ContentCtrl', ['$scope', '$http', 'httpServices','$sce', function ($scope, $http, httpServices, $sce) {
        httpServices.rightCode = '0202';
        $scope.pageSize = httpServices.pageSize;
        $scope.currentPage = httpServices.pageIndex;

        $scope.totalItems = 0;
        $scope.content = {};
        $scope.detailHtml = '';
        $scope.getList = function (e) {
            var btn;
            if (e) {
                btn = $(e.target);
                btn.button('loading');
            }
            httpServices.get('/content/GetList', null, function (resp) {
                if (resp.Success) {
                    resp.Data.length === 0 && common.alert('未查到数据！');
                    $scope.List = resp.Data;
                    $scope.totalItems = resp.TotalCount;
                } else {
                    common.alert(resp.Message || '获取数据失败！');
                }
                btn && btn.button('reset');
            }, function () {
                $scope.totalItems = 0;
                btn && btn.button('reset');
            }, 'queryForm');

        };
        $scope.viewDetail = function (id, e) {
            var btn = $(e.target);
            btn.button('loading');
            httpServices.get('/content/getContent?id=' + id, null, function (resp) {
                if (resp.indexOf('>') > 0) {
                    $scope.detailHtml = $sce.trustAsHtml(resp);
                    ui.detailModel.modal('show');
                } else {
                    common.alert(resp);
                }
                btn.button('reset');
            }, function () {
                common.alert('出现错误或者网络异常！');
                btn.button('reset');
            });
        };

        var ui = {
            detailModel: $('#detailModal')
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
                httpServices.delete('/content/delete', { id: id, t: common.timestamp() }, function (resp) {
                    if (resp.Success) {
                        common.alert('删除成功！');
                        $scope.getList();
                    } else {
                        common.alert(resp.Message || '出现错误或者网络异常！');
                        target.text('删除');
                        target.data('delete', false).css({ 'color': '' });
                    }
                });
            });
        };
    }]);
})(window.angular, document);
