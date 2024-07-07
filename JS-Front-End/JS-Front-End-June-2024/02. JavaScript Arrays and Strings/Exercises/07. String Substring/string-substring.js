function solve(searchWord, text) {
  const regex = new RegExp(`\\b${searchWord}\\b`, 'i');

  if (regex.test(text)) {
    console.log(searchWord);
  } else {
    console.log(`${searchWord} not found!`);
  }
}

solve('python', 'JavaScript is the best programming language');
solve('javascript', 'JavaScript is the best programming language');
