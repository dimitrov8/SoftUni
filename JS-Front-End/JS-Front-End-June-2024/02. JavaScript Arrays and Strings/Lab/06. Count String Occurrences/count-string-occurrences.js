function solve(text, word) {
  let countOfWordOccurrences = 0;

  text.split(' ').forEach((currentWord) => {
    if (currentWord.toLowerCase() === word.toLowerCase()) {
      countOfWordOccurrences++;
    }
  });

  console.log(countOfWordOccurrences);
}

solve('This is a word and it also is a sentence', 'is');
