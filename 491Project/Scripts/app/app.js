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
            $scope.itemName = "";
            $scope.itemDescription = "";
            $scope.itemQuantity = "";
            $scope.itemExpiration = "";
            $scope.itemLocation = "";

            $scope.createNewItem = function () {
                var newItem = new ItemService();
                newItem.Name = $scope.itemName;
                newItem.Description = $scope.itemDescription;
                newItem.Quantity = $scope.itemQuantity;
                newItem.Expires = $scope.itemExpires;
                newItem.$add();
            };
           
            $scope.items = ItemService.query( );
            //console.log($scope.items);

            $scope.item = ItemService.get({itemId: 13},
                function() {
                    //$scope.notes.push("Updating 13");
                    //$scope.item.Quantity = $scope.item.Quantity - 5;//increment by 1
                    $scope.item.$save(); //Save
                });

            $scope.delete = function () {
                var id = this.item.ID;
                ItemService.$remove({ itemId: id }),
                $("#item_" + id).fadeOut();//jquery
            };
        
        }]);


