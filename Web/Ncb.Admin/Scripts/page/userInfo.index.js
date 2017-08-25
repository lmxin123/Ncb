; +(function (angular, doc) {
    'use strict'

    angular.module('deviceApp', ['ui.bootstrap']).controller('DeviceCtrl', function ($scope, $http) {
        $scope.pageSize = 20;
        $scope.totalItems = 0;
        $scope.currentPage = 1;
        $scope.device = {};
        $scope.rec = {};
        $scope.List = [];
        var ui = {
            btnQuery: $('#btnQuery'),
            deviceForm: $('#deviceForm'),
            rechargeForm: $('#rechargeForm'),
            deviceModal: $('#deviceModal'),
            rechargeModal: $('#rechargeModal')
        };

        $('#StartDate,#EndDate,#ExpiryDate').datetimepicker({
            minView: 2,
            maxView: 3,
            format: 'yyyy-mm-dd',
            language: 'zh-CN',
            autoclose: true,
        });
        $scope.getList = function () {
            ui.btnQuery.button('loading');

            $http({
                url: '/device/getList',
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
        $scope.deviceModal = function (id) {
            for (var i = 0; i < $scope.List.length; i++) {
                if ($scope.List[i].ID === id) {
                    $scope.device.ID = id;
                    $scope.device.Name = $scope.List[i].Name;
                    $scope.device.Address = $scope.List[i].Address;
                    $scope.device.CategoryID = $scope.List[i].CategoryID;
                    $scope.device.PhoneNumber = $scope.List[i].PhoneNumber;
                    $scope.device.Gender = $scope.List[i].Gender;
                    $scope.device.RecordState = $scope.List[i].RecordState;
                    $scope.device.Remark = $scope.List[i].Remark;

                    ui.deviceForm.find('#CategoryID').val($scope.device.CategoryID);
                    ui.deviceForm.find('#Gender').val($scope.device.Gender);
                    ui.deviceForm.find('#RecordState').val($scope.device.RecordState);

                    ui.deviceModal.modal('show');
                    break;
                }
            }
        };
        $scope.rechargeModal = function (id) {
            $scope.rec.DeviceId = id;
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
                $http.post('/device/delete', { id: id })
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
            if (!ui.deviceForm.valid()) return;

            var btn = $(e.target);
            btn.button('loading');

            $http({
                url: '/device/update',
                method: 'POST',
                headers: { 'Content-Type': undefined },
                transformRequest: function (data) {
                    data = new FormData(ui.deviceForm[0]);
                    data.append('ID', $scope.device.ID);
                    return data;
                }
            }).success(function (resp) {
                if (resp.Success) {
                    $scope.getList();
                    common.alert("保存成功");
                    ui.deviceModal.modal('hide');
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
                url: '/device/recharge',
                method: 'POST',
                headers: { 'Content-Type': undefined },
                transformRequest: function (data) {
                    data = new FormData(ui.rechargeForm[0]);
                    data.append('DeviceId', $scope.rec.DeviceId);
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
        ui.deviceModal.on('hide.bs.modal', function () {
            $scope.device = {};
        });
    });
})(window.angular, document);