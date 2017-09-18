; +(function (angular, doc) {
    'use strict'

    angular.module('userInfoApp', ['ui.bootstrap', 'common']).controller('userInfoCtrl', ['httpService', function ($scope, $http, httpService) {
        httpService.rightCode = '0303';
        $scope.pageSize = httpServices.pageSize;
        $scope.currentPage = httpServices.pageIndex;

        $scope.totalItems = 0;
        $scope.rec = {};
        $scope.List = [];
        var ui = {
            btnQuery: $('#btnQuery'),
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

            httpService.post('/userinfo/getList', function (resp) {
                if (resp.Success) {
                    resp.Data.length === 0 && common.alert('未查到数据！');
                    $scope.List = resp.Data;
                    $scope.totalItems = resp.TotalCount;
                } else {
                    common.alert(resp.Message);
                }
                ui.btnQuery.button('reset');
            }, function (resp) {
                common.alert('出现错误或者网络异常！');
                $scope.List = [];
                $scope.totalItems = 0;
                ui.btnQuery.button('reset');
            }, 'queryForm');
        };

        $scope.getList();
    }]);
})(window.angular, document);