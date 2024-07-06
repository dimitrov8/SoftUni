function solve(inputArray, numberOfRotations) {
  let arrayLength = inputArray.length;

  numberOfRotations = numberOfRotations % arrayLength;
  let part1 = inputArray.slice(numberOfRotations);
  let part2 = inputArray.slice(0, numberOfRotations);
  let newArray = part1.concat(part2).join(' ');

  console.log(newArray);
}

solve([51, 47, 32, 61, 21], 6);
