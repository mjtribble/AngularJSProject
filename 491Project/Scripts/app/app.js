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


            $scope.items = ItemService.query( );
            //console.log($scope.items);

            $scope.item = ItemService.get({itemId: 13},
                function() {
                    //$scope.notes.push("Updating 13");
                    //$scope.item.Quantity = $scope.item.Quantity - 5;//increment by 1
                    $scope.item.$save(); //Save
                });
            $scope.delete = function() {
                var id = this.item.ID;//this doesn't seem to be working
                ItemService.remove({ itemID: id }, function () {
                    $("#item_" + id).fadeOut();//jquery
                });
            };
            
        }]);


