function findOddOccurrences(input) {
  const wordCounts = {};
  const order = [];

  input
    .toLowerCase()
    .split(' ')
    .forEach((word) => {
      if (!wordCounts[word]) {
        order.push(word);
      }

      wordCounts[word] = (wordCounts[word] || 0) + 1;
    });

  const oddOccurences = order
    .filter((word) => wordCounts[word] % 2 !== 0)
    .join(' ');

  console.log(oddOccurences);
}

findOddOccurrences('Java C# Php PHP Java PhP 3 C# 3 1 5 C#');
