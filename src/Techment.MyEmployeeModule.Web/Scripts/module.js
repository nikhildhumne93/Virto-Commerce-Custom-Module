// Call this to register your module to main application
var moduleName = 'MyEmployeeModule';

if (AppDependencies !== undefined) {
    AppDependencies.push(moduleName);
}

angular.module(moduleName, [])
    .config(['$stateProvider',
        function ($stateProvider) {
            $stateProvider
                .state('workspace.MyEmployeeModuleState', {
                    url: '/MyEmployeeModule',
                    templateUrl: '$(Platform)/Scripts/common/templates/home.tpl.html',
                    controller: [
                        'platformWebApp.bladeNavigationService',
                        function (bladeNavigationService) {
                            var newBlade = {
                                id: 'blade1',
                                controller: 'MyEmployeeModule.helloWorldController',
                                template: 'Modules/$(Techment.MyEmployeeModule)/Scripts/blades/hello-world.html',
                                isClosingDisabled: true,
                            };
                            bladeNavigationService.showBlade(newBlade);
                        }
                    ]
                });
        }
    ])
    .run(['platformWebApp.mainMenuService', '$state',
        function (mainMenuService, $state) {
            //Register module in main menu
            var menuItem = {
                path: 'browse/MyEmployeeModule',
                icon: 'fa fa-user-circle',
                title: 'My Employee Module',
                priority: 100,
                action: function () { $state.go('workspace.MyEmployeeModuleState'); },
                permission: 'MyEmployeeModule:access',
            };
            mainMenuService.addMenuItem(menuItem);
        }
    ]);
