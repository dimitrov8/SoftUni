function solve(text, word) {
  const regex = new RegExp(`${word}`, 'gi');

  let censored = text.replace(regex, '*'.repeat(word.length));

  console.log(censored);
}

solve('Find the hidden word', 'hidden');
