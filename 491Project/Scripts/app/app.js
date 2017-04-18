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
            $scope.delete;

            $scope.newItem = ItemService.save({itemId: 1},
                function () {
                    $scope.newItem.Name = "cheese";
                });
           
            $scope.items = ItemService.query( );
            //console.log($scope.items);

            $scope.item = ItemService.get({itemId: 13},
                function() {
                    //$scope.notes.push("Updating 13");
                    //$scope.item.Quantity = $scope.item.Quantity - 5;//increment by 1
                    $scope.item.$save(); //Save
                });
            $scope.delete = ItemService.remove({ itemID: this.item.ID },
                function () {
                    $("#item_" + id).fadeOut();//jquery
                });
        }]);


