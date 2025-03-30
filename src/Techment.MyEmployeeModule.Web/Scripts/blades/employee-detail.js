angular.module('MyEmployeeModule')
    .controller('MyEmployeeModule.employeeDetailController', ['$scope', 'platformWebApp.bladeNavigationService', '$http', 'MyEmployeeModule.webApi', 'platformWebApp.dialogService',
        function ($scope, bladeNavigationService, $http, api, dialogService) {
            var blade = $scope.blade;
            blade.updatePermission = 'MyEmployeeModule:update';
            blade.subtitle = 'MyEmployeeModule.blades.employee-detail.subtitle';

            blade.refresh = function (parentRefresh) {
                blade.isLoading = true;
                var id = blade.currentEntityId;
                $http.get('api/my-employee-module/getemployeebyid?employeeById=' + id, {})
                    .then(function (data) {
                        var res = data.data.results[0];
                        initializeBlade(res);
                        if (parentRefresh) {
                            blade.parentBlade.refresh();
                        }
                    }, function (error) {
                        bladeNavigationService.setError('Error ' + error.status, blade);
                    });
            }
            function initializeBlade(data) {
                blade.currentEntityId = data.id;
                blade.title = data.firstName + ' ' + data.lastName;

                blade.currentEntity = angular.copy(data);
                blade.origEntity = data;
                blade.isLoading = false;

                //sets security scopes for scope bounded ACL
                if (blade.currentEntity.scopes && angular.isArray(blade.currentEntity.scopes)) {
                    blade.scopes = blade.currentEntity.scopes;
                }
            }
            function isDirty() {
                return !angular.equals(blade.currentEntity, blade.origEntity) && blade.hasUpdatePermission();
            }

            function canSave() {
                return isDirty() && $scope.formScope && $scope.formScope.$valid;
            }

            $scope.saveChanges = function () {
                blade.isLoading = true;

                var entityToSave = angular.copy(blade.currentEntity);
                //api.update({}, entityToSave, data => blade.refresh(true));

                $http.post('api/my-employee-module/update',
                    JSON.stringify(entityToSave)
                ).then(function (data) {
                    blade.refresh(true);
                }, function (error) {
                    bladeNavigationService.setError('Error ' + error.status, blade);
                });
            };
            function deleteEntry() {
                var dialog = {
                    id: "confirmDelete",
                    title: "MyEmployeeModule.dialogs.employee-delete.title",
                    message: "MyEmployeeModule.dialogs.employee-delete.message",
                    callback: function (remove) {
                        if (remove) {
                            blade.isLoading = true;

                            //api.remove({ ids: blade.currentEntityId }, function () {
                            //    $scope.bladeClose();
                            //    blade.parentBlade.refresh();
                            //});
                            $http.post('api/my-employee-module/remove?ids=' + blade.currentEntityId, {})
                                .then(function (data) {
                                    $scope.bladeClose();
                                    blade.parentBlade.refresh();
                                }, function (error) {
                                    bladeNavigationService.setError('Error ' + error.status, blade);
                                });
                        }
                    }
                }
                dialogService.showConfirmationDialog(dialog);
            }
            function reset() {
                angular.copy(blade.origEntity, blade.currentEntity);
                if (blade.childrenBlades) {
                    _.each(blade.childrenBlades, function (x) {
                        if (x.refresh) {
                            x.refresh(blade.currentEntity);
                        }
                    });
                }
            }

            $scope.setForm = function (form) { $scope.formScope = form; };

            blade.onClose = function (closeCallback) {
                bladeNavigationService.showConfirmationIfNeeded(isDirty(), canSave(), blade, $scope.saveChanges, closeCallback, "MyEmployeeModule.dialogs.employee-save.title", "MyEmployeeModule.dialogs.employee-save.message");
            };
            blade.headIcon = 'fa fa-user-circle';
            blade.toolbarCommands = [
                {
                    name: "platform.commands.save",
                    icon: 'fas fa-save',
                    executeMethod: $scope.saveChanges,
                    canExecuteMethod: canSave,
                    permission: blade.updatePermission
                },
                {
                    name: "platform.commands.reset",
                    icon: 'fa fa-undo',
                    executeMethod: reset,
                    canExecuteMethod: isDirty,
                    permission: blade.updatePermission
                },
                {
                    name: "platform.commands.delete", icon: 'fas fa-trash-alt',
                    executeMethod: deleteEntry,
                    canExecuteMethod: function () { return true; },
                    permission: 'MyEmployeeModule:delete'
                }
            ];

            $scope.$on("refresh-entity-by-id", function (event, id) {
                if (blade.currentEntityId === id) {
                    bladeNavigationService.closeChildrenBlades(blade, blade.refresh);
                }
            });

            blade.refresh();
        }]);
