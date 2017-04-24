angular.module('myApp', ['myApp.Services', 'timer'])
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
                newItem.LocationID = $scope.itemLocation;

                var test = newItem.$add();
                test.then(function (a){
                    $scope.items.Items.push(a);
                });
            };

            $scope.addLocation = function () {
                var newLoc = new LocationService();
                newLoc.Name = $scope.locationName;
                newLoc.Description = $scope.locationDescription;
                newLoc.ID = $scope.locationID;

                var add = newLoc.$add()
                add.then(function (resp) {
                    $scope.locations.Items.push(resp);
                });
            };
            
            $scope.editItem = function () {
                $('.#item_name', myModal).val(item.Name);
                $('.#item_description', myModal).val(item.Desription);
                $('.itemQuantity', myModal).val(item.Quantity);
                $('.itemExpiration', myModal).val(item.Expires);
                $('.itemLocation', myModal).val($scope.location(item.LocationID));
                $scope.itm.$save();
            };

            $scope.editLocation = function (loc) {
                $('.locationName', anotherModal).val(location.Name);
                $('.locationDescription', anotherModal).val(location.Desription);
                $scope.loc.$save();
            };

            $scope.deleteItem = function (item) {
                var id = item.ID;
                ItemService.remove({ itemId: id }),
                $("#item_" + id).fadeOut();//jquery
            };

            $scope.deleteLocation = function (location) {
                var id = location.ID;
                LocationService.remove({ locationId: id }),
                $("#location_" + id).fadeOut();//jquery
            };
        }]);
