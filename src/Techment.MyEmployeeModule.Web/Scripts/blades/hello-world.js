angular.module('MyEmployeeModule')
    .controller('MyEmployeeModule.helloWorldController', ['$scope', '$http', 'MyEmployeeModule.webApi', 'platformWebApp.bladeUtils', 'uiGridConstants', 'platformWebApp.uiGridHelper',
        function ($scope, $http, api, bladeUtils, uiGridConstants, uiGridHelper) {
            $scope.uiGridConstants = uiGridConstants;


            var blade = $scope.blade;
            blade.title = 'My Employee Module';
            var bladeNavigationService = bladeUtils.bladeNavigationService;

            blade.refresh = function () {
                blade.isLoading = true;
                $http.post('api/my-employee-module/search', {
                    keyword: filter.keyword ? filter.keyword : undefined,
                    sort: uiGridHelper.getSortExpression($scope),
                    skip: ($scope.pageSettings.currentPage - 1) * $scope.pageSettings.itemsPerPageCount,
                    take: $scope.pageSettings.itemsPerPageCount
                }).then(function (data) {
                    blade.isLoading = false;
                    $scope.pageSettings.totalItems = data.data.totalCount;
                    blade.currentEntities = data.data.results;
                }, function (error) {
                    bladeNavigationService.setError('Error ' + error.status, blade);
                });
            }

            blade.selectNode = function (data) {
                $scope.selectedNodeId = data.id;

                var newBlade = {
                    id: 'employeeDetails',
                    currentEntityId: data.id,
                    currentEntity: data,//
                    title: data.name,
                    controller: 'MyEmployeeModule.employeeDetailController',
                    template: 'Modules/$(Techment.MyEmployeeModule)/Scripts/blades/employee-detail.tpl.html'
                };
                bladeNavigationService.showBlade(newBlade, blade);
            }

            function openBladeNew() {
                $scope.selectedNodeId = null;

                var newBlade = {
                    id: 'employeeDetails',
                    currentEntity: {},
                    title: 'MyEmployeeModule.blades.new-employee.title',
                    subtitle: 'MyEmployeeModule.blades.new-employee.subtitle',
                    controller: 'MyEmployeeModule.newEmployeeDetailController',
                    template: 'Modules/$(Techment.MyEmployeeModule)/Scripts/blades/new-employee-detail.tpl.html'
                };
                bladeNavigationService.showBlade(newBlade, blade);
            }
            blade.headIcon = 'fa fa-user-circle';

            blade.toolbarCommands = [
                {
                    name: "platform.commands.refresh", icon: 'fa fa-refresh',
                    executeMethod: blade.refresh,
                    canExecuteMethod: function () {
                        return true;
                    }
                },
                {
                    name: "platform.commands.add", icon: 'fas fa-plus',
                    executeMethod: openBladeNew,
                    canExecuteMethod: function () {
                        return true;
                    },
                    permission: 'MyEmployeeModule:create'
                }
            ];

            // simple and advanced filtering
            var filter = $scope.filter = {};

            filter.criteriaChanged = function () {
                if ($scope.pageSettings.currentPage > 1) {
                    $scope.pageSettings.currentPage = 1;
                } else {
                    blade.refresh();
                }
            };

            // ui-grid
            $scope.setGridOptions = function (gridOptions) {
                uiGridHelper.initialize($scope, gridOptions, function (gridApi) {
                    uiGridHelper.bindRefreshOnSortChanged($scope);
                });
                bladeUtils.initializePagination($scope);
            };

            //blade.refresh = function () {
            //    api.get(function (data) {
            //        blade.title = 'MyEmployeeModule.blades.hello-world.title';
            //        blade.data = data.result;
            //        blade.isLoading = false;
            //    });
            //};

            //blade.refresh();
        }]);
