function solve(inputString) {
  const pattern = /#[A-Za-z]+/g;
  const matches = inputString.match(pattern);
  let hashtagWords = [];

  if (matches) {
    hashtagWords = matches.map((match) => match.slice(1));
  }

  console.log(hashtagWords.join('\r\n'));
}

solve(
  'The symbol # is known #variously in English-speaking #regions as the #number sign'
);
