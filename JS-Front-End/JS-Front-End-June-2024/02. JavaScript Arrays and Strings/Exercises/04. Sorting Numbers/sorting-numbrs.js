function solve(inputArray) {
  inputArray.sort((a, b) => a - b);

  let left = 0;
  let right = inputArray.length - 1;

  let result = [];
  for (let index = 0; index < inputArray.length; index++) {
    if (index % 2 == 0) {
      result.push(inputArray[left]);
      left++;
    } else {
      result.push(inputArray[right]);
      right--;
    }
  }

  return result;
}

console.log(solve([1, 65, 3, 52, 48, 63, 31, -3, 18, 56]));
