(function () {
    angular
        .module("MyApp.Services", ["ngResource"])
        .factory
        ("MyApp.Services",
        ["$resource",
            function ($resource) {
                return $resource
                (
                    "http://msucs491-spring17-8d42d07c-01e3-4d92-a661-46d9fa7700bb.azurewebsites.net/api/item/:itemId",
                    { itemId: '@id' },
                    {
                        query:
                        {
                            method: "GET",
                            isArray: false
                        }
                    }
                );
            }
        ]
        )
})();