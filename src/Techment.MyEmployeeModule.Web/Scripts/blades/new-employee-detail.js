angular.module('MyEmployeeModule')
    .controller('MyEmployeeModule.newEmployeeDetailController', ['$scope', 'platformWebApp.bladeNavigationService', '$http', 'MyEmployeeModule.webApi', 'platformWebApp.dialogService',
        function ($scope, bladeNavigationService, $http, api, dialogService) {
            var blade = $scope.blade;

            function initializeBlade(data) {
                blade.currentEntityId = data.id;

                blade.currentEntity = angular.copy(data);
                blade.origEntity = data;
                blade.isLoading = false;
                //blade.currentEntity.validationRuleCodePattern = "^[a-zA-Z0-9_\-]*$";
            };
            $scope.saveChanges = function () {
                blade.isLoading = true;
               
                api.save(blade.currentEntity, function (data) {
                    blade.parentBlade.refresh();
                    blade.parentBlade.selectNode(data);
                },
                    function (error) { bladeNavigationService.setError('Error ' + error.status, blade); });
            };

            $scope.setForm = function (form) {
                $scope.formScope = form;
            }
             
            initializeBlade(blade.currentEntity);
        }]);
