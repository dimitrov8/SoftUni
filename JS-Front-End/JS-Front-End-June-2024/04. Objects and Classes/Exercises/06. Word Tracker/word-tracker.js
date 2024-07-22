function wordsTracker(inputArray) {
  const wantedWords = inputArray.shift().split(' ');
  const wantedWordsCounts = {};
  wantedWords.forEach((word) => (wantedWordsCounts[word] = 0));

  inputArray.forEach(
    (word) => word in wantedWordsCounts && wantedWordsCounts[word]++
  );
  wantedWords.sort((a, b) => wantedWordsCounts[b] - wantedWordsCounts[a]);
  wantedWords.forEach((word) =>
    console.log(`${word} - ${wantedWordsCounts[word]}`)
  );
}

wordsTracker([
  'this sentence',
  'In',
  'this',
  'sentence',
  'you',
  'have',
  'to',
  'count',
  'the',
  'occurrences',
  'of',
  'the',
  'words',
  'this',
  'and',
  'sentence',
  'because',
  'this',
  'is',
  'your',
  'task',
]);
