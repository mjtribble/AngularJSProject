(function () {
    angular
        .module('myApp.Services', ['ngResource'])
        .factory("myApp.Services.ItemService", ["$resource",
            function ($resource) {
                return $resource("/api/item/:itemId",
                    { itemId: '@ID' },
                    {
                        'add': { method: 'POST' },
                        'get': { method: 'GET' },
                        'query': { method: 'GET', isArray: false, },
                        'save': { method: 'PATCH' },
                        'remove': { method: 'DELETE' },
                    }
                );
            }
        ])
        .factory("myApp.Services.LocationService", ["$resource",
            function ($resource) {
                return $resource("/api/location/:locationId",
                    { locationId: '@ID' },
                    {
                        'add': { method: 'POST' },
                        'get': { method: 'GET' },
                        'query': { method: 'GET', isArray: false },
                        'save': { method: 'PATCH' },
                        'remove': { method: 'DELETE' },
                    }
                );
            }
        ])
})();