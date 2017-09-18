(function (angular, doc) {
    function factory($http) {


        var http = function (method, url, params, success, error, formId, index, pageSize) {
            var options = {
                method: method.toUpperCase(),
                url: url
            };

            if (formId) {
                options.method = 'POST';
                options.headers = { 'Content-Type': undefined };
                options.transformRequest = function (data) {
                    data = new FormData(doc.getElementById(formId));
                    data.append('pageIndex', index);
                    data.append('pageSize', pageSize);
                    if (params) {
                        for (var p in params) {
                            data.append(p, params[p]);
                        }
                    }
                    return data;
                };
            }
            else if (options.method === 'GET') {
                if (params)
                    options.params = params;
            }
            else {
                if (params)
                    options.params = params;
            }

            $http(options).success(function (resp) {
                success && success(resp);
            }).error(function () {
                common.alert('出现错误或者网络异常！');
                error && error();
            });
        };

        return {
            rightCode: '',
            pageIndex: 1,
            pageSize: 20,
            get: function (url, params, success, error, formId) {
                if (params) {
                    params.rightCode = this.rightCode;
                }
                else {
                    params = {
                        rightCode: this.rightCode
                    };
                }
                http('GET', url, params, success, error, formId, this.pageIndex, this.pageSize);
            },
            post: function (url, params, success, error, formId) {
                if (params) {
                    params.rightCode = this.rightCode;
                }
                else {
                    params = {
                        rightCode: this.rightCode
                    };
                }
                http('POST', url, params, success, error, formId, this.pageIndex, this.pageSize);
            },
            delete: function (url, params, success, error, formId) {
                if (params) {
                    params.rightCode = this.rightCode;
                }
                else {
                    params = {
                        rightCode: this.rightCode
                    };
                }
                http('DELETE', url, params, success, error, formId, this.pageIndex, this.pageSize);
            }
        };
    }

    factory.$inject = ['$http'];

    angular.module('common', []).factory('httpServices', factory)
})(window.angular, document)