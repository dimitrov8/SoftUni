function solve(namesArray) {
  namesArray
    .sort((a, b) => a.toLowerCase().localeCompare(b.toLowerCase()))
    .map((name, index) => {
      console.log(`${index + 1}.${name}`);
    });
}

solve(['John', 'Bob', 'Christina', 'Ema']);
