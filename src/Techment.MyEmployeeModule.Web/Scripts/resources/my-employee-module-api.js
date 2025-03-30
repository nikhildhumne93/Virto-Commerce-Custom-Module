angular.module('MyEmployeeModule')
    .factory('MyEmployeeModule.webApi', ['$resource', function ($resource) {
        return $resource('api/my-employee-module');
    }]);
