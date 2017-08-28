; +(function (angular, doc) {
    'use strict'

    angular.module('userInfoApp', ['ui.bootstrap']).controller('userInfoCtrl', function ($scope, $http) {
        $scope.pageSize = 20;
        $scope.totalItems = 0;
        $scope.currentPage = 1;
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
            ui.btnQuery.button('loading');

            $http({
                url: '/userinfo/getList',
                method: 'POST',
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
                    common.alert(resp.Message);
                }
                ui.btnQuery.button('reset');
            }).error(function (resp) {
                common.alert('出现错误或者网络异常！');
                $scope.List = [];
                $scope.totalItems = 0;
                ui.btnQuery.button('reset');
            });
        };
        $scope.userInfoModal = function (userInfo) {
            $scope.userInfo = userInfo;

            ui.userInfoForm.find('#CategoryID').val(userInfo.CategoryID);
            ui.userInfoForm.find('#Gender').val($scope.userInfo.Gender);
            ui.userInfoForm.find('#RecordState').val(userInfo.RecordState);

            ui.userInfoModal.modal('show');
        };
        $scope.rechargeModal = function (userInfo) {
            $scope.rec.id = userInfo.ID;
            $scope.rec.name =  userInfo.Name;
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
                $http.post('/userinfo/delete', { id: id })
                    .success(function (resp) {
                        if (resp.Success) {
                            common.alert('删除成功！');
                            $scope.getList();
                        } else {
                            common.alert(resp.Message);
                            target.text('删除');
                            target.data('delete', false).css({ 'color': '' });
                        }
                    }).error(function () {
                        common.alert('出现错误或者网络异常！');
                        $scope.List = [];
                        $scope.totalItems = 0;
                    });
            });
        };
        $scope.save = function (e) {
            if (!ui.userInfoForm.valid()) return;

            var btn = $(e.target);
            btn.button('loading');

            $http({
                url: '/userinfo/update',
                method: 'POST',
                headers: { 'Content-Type': undefined },
                transformRequest: function (data) {
                    data = new FormData(ui.userInfoForm[0]);
                    data.append('ID', $scope.userInfo.ID);
                    return data;
                }
            }).success(function (resp) {
                if (resp.Success) {
                    $scope.getList();
                    common.alert("保存成功");
                    ui.userInfoModal.modal('hide');
                } else {
                    common.alert(resp.Message);
                }
                btn.button('reset');
            }).error(function (resp) {
                common.alert('出现错误或者网络异常！');
                btn.button('reset');
            });
        }
        $scope.recharge = function (e) {
            if (!ui.rechargeForm.valid()) return;

            var btn = $(e.target).button('loading');

            $http({
                url: '/userinfo/recharge',
                method: 'POST',
                headers: { 'Content-Type': undefined },
                transformRequest: function (data) {
                    data = new FormData(ui.rechargeForm[0]);
                    data.append('id', $scope.rec.id);
                    data.append('Month', ui.radioContainer.find('input:checked').attr('id'));
                    return data;
                }
            }).success(function (resp) {
                if (resp.Success) {
                    $scope.getList();
                    common.alert("充值成功!");
                    ui.rechargeModal.modal('hide');
                } else {
                    common.alert(resp.Message);
                }
                btn.button('reset');
            }).error(function (resp) {
                common.alert('出现错误或者网络异常！');
                btn.button('reset');
            });
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
    });
})(window.angular, document);