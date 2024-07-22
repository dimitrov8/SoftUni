function manageMovies(inputArray) {
  const movies = [];

  function extractMovieName(args, startIndex, endIndex) {
    return args.slice(startIndex, endIndex).join(' ');
  }

  function extractDirectorName(args, startIndex) {
    return args.slice(startIndex + 1).join(' ');
  }

  function findMovie(name) {
    return movies.find((movie) => movie.name === name);
  }

  function extractDate(args, startIndex) {
    return args.slice(startIndex + 1).join(' ');
  }

  inputArray.forEach((input) => {
    const args = input.split(' ');

    if (input.includes('addMovie')) {
      const movieName = extractMovieName(args, 1);

      if (!findMovie(movieName)) {
        movies.push({ name: movieName, director: null, date: null });
      }
    } else if (input.includes('directedBy')) {
      const directedByIndex = args.indexOf('directedBy');
      const movieName = extractMovieName(args, 0, directedByIndex);
      const movie = findMovie(movieName);

      if (movie) {
        const director = extractDirectorName(args, directedByIndex);
        movie.director = director;
      }
    } else if (input.includes('onDate')) {
      const onDateIndex = args.indexOf('onDate');
      const movieName = extractMovieName(args, 0, onDateIndex);
      const movie = findMovie(movieName);

      if (movie) {
        const date = extractDate(args, onDateIndex);
        movie.date = date;
      }
    }
  });

  movies
    .filter((movie) => movie.name && movie.director && movie.date)
    .forEach((movie) => {
      console.log(JSON.stringify(movie));
    });
}

manageMovies([
  'addMovie Fast and Furious',
  'addMovie Godfather',
  'Inception directedBy Christopher Nolan',
  'Godfather directedBy Francis Ford Coppola',
  'Godfather onDate 29.07.2018',
  'Fast and Furious onDate 30.07.2018',
  'Batman onDate 01.08.2018',
  'Fast and Furious directedBy Rob Cohen',
]);
