; +(function (angular, doc) {
    'use strict'

    angular.module('userInfoApp', ['ui.bootstrap', 'common']).controller('UserInfoCtrl', ['$scope', '$http', 'httpServices', function ($scope, $http, httpServices) {
        httpServices.rightCode = '0302';
        $scope.pageSize = httpServices.pageSize;
        $scope.currentPage = httpServices.pageIndex;

        $scope.totalItems = 0;
        $scope.category = {};
        $scope.List = [];
        $scope.getList = function () {
            httpServices.post('/userInfoCategory/getList', {
                pageSize: $scope.pageSize,
                pageIndex: $scope.currentPage
            }, function (resp) {
                if (resp.Success) {
                    resp.Data.length === 0 && common.alert('未查到数据！');
                    $scope.List = resp.Data;
                    $scope.totalItems = resp.TotalCount;
                } else {
                    common.alert(resp.Message);
                }
            }, function (resp) {
                $scope.List = [];
                $scope.totalItems = 0;
            });
        };

        $scope.getList();

        var ui = {
            categroyModal: $('#categoryModal')
        };

        $scope.categoryModal = function () {
            if (arguments.length != 0) {
                for (var i = 0; i < $scope.List.length; i++) {
                    if ($scope.List[i].ID == arguments[0]) {
                        $scope.category.ID = $scope.List[i].ID;
                        $scope.category.Name = $scope.List[i].Name;
                        $scope.category.Remark = $scope.List[i].Remark;
                        break;
                    }
                }
            }
            ui.categroyModal.modal('show');
        }

        $scope.delete = function (id, e) {
            var target = $(e.target);

            if (!target.data('delete')) {
                target.text('确定要删除吗');
                target.data('delete', true).css({ 'color': 'red' });
                return;
            }

            common.alert("正在删除...", function () {
                httpServices.delete('/userInfoCategory/delete', { id: id }, function (resp) {
                    if (resp.Success) {
                        common.alert('删除成功！');
                        $scope.getList();
                    } else {
                        common.alert(resp.Message);
                        target.text('删除');
                        target.data('delete', false).css({ 'color': '' });
                    }
                }, function (resp) {
                    common.alert(resp.Message || '出现错误或者网络异常！');
                    $scope.List = [];
                    $scope.totalItems = 0;
                });
            });
        };

        $('#btnCheck').ajaxClick({
            formSelector: '#categoryForm',
            onBefore: function (self) {
                self.$form.attr('action', '/userInfoCategory/' + ($scope.category.ID ? 'update' : 'create'));
                return {
                    rightCode: httpService.rightCode
                };
            },
            onCompleted: function (result, $ajaxBtn) {
                if (result.Success) {
                    $scope.getList();
                    common.alert("保存成功");
                    ui.categroyModal.modal('hide');
                }
                else {

                    common.alert("保存失败：" + result.Message)
                }
                $ajaxBtn.reset();
            }
        });
        ui.categroyModal.on('hide.bs.modal', function () {
            $scope.category = {};
        });
    }]);
})(window.angular, document);