function solve(inputArray, nSteps) {
  let outputArray = [];
  for (let index = 0; index < inputArray.length; index += nSteps) {
    const currentElement = inputArray[index];
    outputArray.push(currentElement);
  }

  return outputArray;
}

const result = solve(['5', '20', '31', '4', '20'], 2);
console.log(result);
