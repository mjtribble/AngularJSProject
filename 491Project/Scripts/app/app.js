angular.module('myApp', ['myApp.Services'])
    .controller('listController', ['$scope', 'myApp.Services.ItemService',
        function ($scope, ItemService) {
            $scope.sortType = 'LocationID';
            $scope.sortReverse = false;
            $scope.searchFridge = '';
            $scope.notes = [];
            $scope.results = [];
            $scope.item = null;
            $scope.items = [];


            $scope.items = ItemService.query();
            $scope.addItem = function () {
                $scope.items.push({ 'name': $scope.name, 'employees': $scope.employees, 'headoffice': $scope.headoffice });
                $scope.name = '';
                $scope.employees = '';
                $scope.headoffice = '';
            };
            console.log($scope.items);

            $scope.item = ItemService.get({itemId: 13},
                function() {
                    //$scope.notes.push("Updating 13");
                    $scope.item.Quantity = $scope.item.Quantity + 1;//increment by 1
                    $scope.item.$save(); //Save
                });
        }]);


