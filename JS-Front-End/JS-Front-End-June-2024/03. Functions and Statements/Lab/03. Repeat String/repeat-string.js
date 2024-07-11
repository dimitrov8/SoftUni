function repeatString(strInput, repeatCount) {
  let output = strInput;
  for (let index = 0; index < repeatCount - 1; index++) {
    output += strInput;
  }

  console.log(output);
}

repeatString('abc', 3);
