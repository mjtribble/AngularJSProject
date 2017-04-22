angular.module('myApp', ['myApp.Services'])
    .controller('listController', ['$scope', '$location', 'myApp.Services.ItemService', 'myApp.Services.LocationService',
        function ($scope, $location, ItemService, LocationService) {
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
            
            //$('#anotherModal').modal('toggle');

            //$('#myModal').modal('toggle');

            $scope.items =  ItemService.query();
            $scope.locationsByID = {};

            $scope.locations = LocationService.query( 
                function (data) {
                    for (var i = 0; i < data.Items.length; i++) {
                        $scope.locationsByID[data.Items[i].ID] = data.Items[i];
                    }
                })


            //$scope.addItem = function ()
            //{
            //    ItemService.add($scope.item, function () 
            //    {
            //        $location.reload();//how do I reload the page?
            //    });
            //};

            $scope.addItem = function () {
                var newItem = new ItemService();
                newItem.Name = $scope.itemName;
                newItem.Description = $scope.itemDescription;
                newItem.Quantity = $scope.itemQuantity;
                newItem.Expires = $scope.itemExpires;
                //newItem.LocationID = 1;

                var test = newItem.$add();
                test.then(function (a){
                    $scope.items.Items.push(a);
                });
            };

            $scope.addLocation = function ()
            {
                LocationService.add($scope.location)
            }
            
            $scope.editItem = function (itm) {//is this how I send in this item?
                $('.itemName', myModal).val(item.Name);
                $('.itemDescription', myModal).val(item.Desription);
                $('.itemQuantity', myModal).val(item.Quantity);
                $('.itemExpiration', myModal).val(item.Expires);
                $('.itemLocation', myModal).val($scope.location(item.LocationID));//not sure how to edit the location by Name
                $scope.itm.$save();
            };

            $scope.deleteItem = function (item) {
                var id = item.ID;
                ItemService.remove({ itemId: id }),
                $("#item_" + id).fadeOut();//jquery
            };        
        }]);
