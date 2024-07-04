function solve(inputString, startIndex, count) {
  const start = Math.max(0, Math.min(inputString.length, startIndex));
  const end = Math.min(inputString.length, start + count);

  let output = inputString.substring(start, end);

  console.log(output);
}

solve('SkipWord', 4, 7);
