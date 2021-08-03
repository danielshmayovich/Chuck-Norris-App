function JokesController($scope, ApiJokesService)
{
    function shuffleJoke()
    {
        var randomIndex = Math.floor(Math.random() * allJokes.length);

        return allJokes[randomIndex];
    }

    function voteJoke(jokeId)
    {
        console.log(jokeId);
        api.postLike(jokeId, function (valid)
        {
            console.log(valid);
        });
    }

    var api = ApiJokesService();

    var allJokes;

    api.getJokes(function (jokes)
    {
        console.log(jokes);
        allJokes = jokes;

        console.log(shuffleJoke());
        $scope.shuffleJoke();
    });

    $scope.shuffleJoke = function()
    {
        $scope.joke = shuffleJoke();
    };

    $scope.voteJoke = voteJoke; // delegate here!!!
}