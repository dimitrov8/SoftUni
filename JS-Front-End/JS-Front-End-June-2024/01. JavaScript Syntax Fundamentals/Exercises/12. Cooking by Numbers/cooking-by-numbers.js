function solve(number, arg1, arg2, arg3, arg4, arg5) {
  let result = number;
  let args = [arg1, arg2, arg3, arg4, arg5];
  let output = [];

  for (let currentArg of args) {
    if (currentArg === 'chop') {
      result /= 2;
    } else if (currentArg === 'dice') {
      result = Math.sqrt(result);
    } else if (currentArg === 'spice') {
      result++;
    } else if (currentArg === 'bake') {
      result *= 3;
    } else if (currentArg === 'fillet') {
      result = result - (20 / 100) * result;
    }
    output.push(result);
  }

  console.log(output.join('\r\n'));
}

solve('32', 'chop', 'chop', 'chop', 'chop', 'chop');
