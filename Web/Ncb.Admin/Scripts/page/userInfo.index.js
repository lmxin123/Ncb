; +(function (angular, doc) {
    'use strict'
    angular.module('userInfoApp', ['ui.bootstrap', 'common']).controller('userInfoCtrl', ['$scope', '$http', 'httpServices', function ($scope, $http, httpServices) {
        httpServices.rightCode = '0301';
        $scope.pageSize = httpServices.pageSize;
        $scope.currentPage = httpServices.pageIndex;

        $scope.userInfo = {};
        $scope.rec = {};
        $scope.List = [];
        var ui = {
            btnQuery: $('#btnQuery'),
            userInfoForm: $('#userInfoForm'),
            rechargeForm: $('#rechargeForm'),
            userInfoModal: $('#userInfoModal'),
            rechargeModal: $('#rechargeModal'),
            radioContainer: $('#radioContainer'),
            expiryDate: $('#ExpiryDate')
        };

        var defaultDateOptions = {
            minView: 2,
            maxView: 3,
            format: 'yyyy-mm-dd',
            language: 'zh-CN',
            autoclose: true,
        };

        $('#StartDate,#EndDate').datetimepicker(defaultDateOptions);

        $scope.getList = function () {
            var btn = ui.btnQuery.button('loading');
            httpServices.post('/userinfo/getList', null, function (resp) {
                if (resp.Success) {
                    resp.Data.length === 0 && common.alert('未查到数据！');
                    $scope.List = resp.Data;
                    $scope.totalItems = resp.TotalCount;
                } else {
                    common.alert(resp.Message);
                }
                btn.button('reset');
            }, function (resp) {
                common.alert('出现错误或者网络异常！');
                $scope.List = [];
                $scope.totalItems = 0;
                btn.button('reset');
            }, 'queryForm');

        };
        $scope.userInfoModal = function (userInfo) {
            $scope.userInfo = userInfo;

            ui.userInfoForm.find('#CategoryID').val(userInfo.CategoryID);
            ui.userInfoForm.find('#Gender').val(userInfo.Gender);
            ui.userInfoForm.find('#RecordState').val(userInfo.RecordState);

            ui.userInfoModal.modal('show');
        };
        $scope.rechargeModal = function (userInfo) {
            $scope.rec.id = userInfo.ID;
            $scope.rec.name = userInfo.Name;
            $scope.rec.ExpiryDate = userInfo.ExpiryDate;

            $scope.userInfo = userInfo;
            ui.radioContainer.find('input:checked').trigger('click');
            ui.rechargeModal.modal('show');
        };
        $scope.delete = function (id, e) {
            var target = $(e.target);

            if (!target.data('delete')) {
                target.text('确定要删除吗');
                target.data('delete', true).css({ 'color': 'red' });
                return;
            }
            common.alert("正在删除...", function () {
                httpServices.delete('/userinfo/delete', { id: id, t: common.timestamp() }, function (resp) {
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
        $scope.save = function (e) {
            if (!ui.userInfoForm.valid()) return;

            var btn = $(e.target);
            btn.button('loading');
            httpServices.post('/userinfo/update', null, function (resp) {
                if (resp.Success) {
                    $scope.getList();
                    common.alert("保存成功");
                    ui.userInfoModal.modal('hide');
                } else {
                    common.alert(resp.Message);
                }
                btn.button('reset');
            }, function (resp) {
                common.alert(resp.Message || '出现错误或者网络异常！');
                btn.button('reset');
                }, ui.userInfoForm[0].id);
        }

        $scope.recharge = function (e) {
            if (!ui.rechargeForm.valid()) return;

            var btn = $(e.target).button('loading');

            httpServices.post('/userinfo/recharge', null, function (resp) {
                if (resp.Success) {
                    $scope.getList();
                    common.alert("充值成功!");
                    ui.rechargeModal.modal('hide');
                } else {
                    common.alert(resp.Message);
                }
                btn.button('reset');
            }, function (resp) {
                common.alert(resp.Message || '出现错误或者网络异常！');
                btn.button('reset');
            }, ui.rechargeForm[0].id);
        };

        ui.userInfoModal.on('hide.bs.modal', function () {
            $scope.userInfo = {};
        });
        ui.radioContainer.find('input').click(function () {
            var index = parseInt(this.id),
                expiryDate = $scope.userInfo.ExpiryDate,
                date = new Date();

            if (expiryDate != null) {
                date = new Date(expiryDate);
            }
            date.setMonth(date.getMonth() + index);
            $scope.rec.ExpiryDate = date.getFullYear() + '-' + (date.getMonth() + 1) + '-' + date.getDate();
            ui.expiryDate.val($scope.rec.ExpiryDate);
        });

        $scope.getList();
    }]);
})(window.angular, document);