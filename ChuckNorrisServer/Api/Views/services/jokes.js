function ApiJokesService($http)
{
    function Service()
    {
        this.getJokes = function (ok, ko) 
        {
            $http.get('/api/jokes/get').then(function (response) 
            {
                //console.log(response);
                ok(response.data.map(function (item)
                {
                    return new JokeModel(item.Id, item.Body);
                }));

            }, function (error)
            {
                if (ko) ko(error);
            });
        };

        this.postLike = function(id, ok, ko)
        {
            var data = new JokeModel(id);
            
            $http.post('/api/joke/votes/post', data).then(function (response)
            {
                ok(response.data);

            }, function (error)
            {
                if (ko) ko(error);
            });
        };
    }

    var service;

    return function()
    {
        if (!service) service = new Service();

        return service;
    }
}