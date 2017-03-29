angular.module('myApp', ['ngResource'])
    .controller('listController', ['$scope', '$http', '$resource',
        function ($scope, $http, $resource) {
            $scope.notes = [];
            $scope.results = [];
            $scope.item = null;
            $scope.locations = [
                {id: 0,label: "Refrigerator"},
                {id: 1,label: "Outdoor Fridge"},
                {id: 2,label: "Cabinet A"},
                {id: 3,label: "Pantry"}
            ];
            $scope.items = [
                {id: 1, name: "Milk", description: "Cow juice", quantity: 1, expires: "2.26.17 7:00AM", location: "Refrigerator"},
                {id: 1, name: "Milk", description: "Cow juice", quantity: 1, expires: "2.26.17 7:00AM", location: "Refrigerator"},
                {id: 1, name: "Milk", description: "Cow juice", quantity: 1, expires: "2.26.17 7:00AM", location: "Refrigerator"},
                {id: 1, name: "Milk", description: "Cow juice", quantity: 1, expires: "2.26.17 7:00AM", location: "Refrigerator"}
            ];
            $scope.notes.push("Calling...");

            var ItemService = $resource('https://msucs491-spring17-assignment2.azurewebsites.net/api/item/:itemId', 
                { itemID:'@ID'}, 
                {
                    'get': {method:'GET'},
                    'query': {method:'GET', isArray:false },
                    'save': { method: 'PATCH' },
                    'remove': {method: 'DELETE'},
                });
            $scope.results = ItemService.query();
            console.log($scope.results);

            $scope.notes.push("Fetching 24");

            $scope.item = ItemService.get({itemID:24},
                function() {
                    $scope.notes.push("Updating 24");
                    $scope.item.Quantity = $scope.item.Quantity + 1;//increment by 1
                    $scope.notes.push("Saving Item ID 24");
                    $scope.item.$save(); //Save
                });

            //$http({
            //    method: 'GET',
            //    url: 'https://randomuser.me/api/?results=10'
            //}).then(function successCallback(response) {
            //    $scope.notes.push("In Success Callback");
            //    $scope.results = response.data.results;
            //}, function errorCallback(response) {
            //    $scope.notes.push("In Error Callback");
            //    $scope.results = [];
            //});

        }]);

//$scope.$watch("filter.One + filter.Two",
//    function (newVal, oldVal) {
//        if (newVal == "FooBar") {
//            alert('Filters are set to foobar')
//        }
//    });

