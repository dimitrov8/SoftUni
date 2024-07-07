function solve(inputWords, sentance) {
  let words = inputWords.split(',');
  let sentenceArray = sentance.split(' ');

  sentenceArray.forEach((template, index) => {
    if (template.includes('*')) {
      words.forEach((word) => {
        if (word.length == template.length) {
          sentenceArray[index] = word;
          words = words.filter((w) => w !== word);
        }
      });
    }
  });

  const result = sentenceArray.join(' ');

  console.log(result);
}

solve('great', 'softuni is ***** place for learning new programming languages');
