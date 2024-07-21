function validatePassoword(password) {
  const minLength = 6;
  const maxLength = 10;
  const regex = /^[a-zA-Z0-9]+$/;
  const minDigits = 2;

  let errors = [];

  if (minLength > password.length || maxLength < password.length) {
    errors.push('Password must be between 6 and 10 characters');
  }

  if (regex.test(password) === false) {
    errors.push('Password must consist only of letters and digits');
  }

  const digitMatches = password.match(/\d/g);
  const digitCount = (digitMatches || []).length;

  if (digitCount < minDigits) {
    errors.push('Password must have at least 2 digits');
  }

  if (errors.length > 0) {
    errors.forEach((error) => {
      console.log(error);
    });
    return;
  }

  console.log('Password is valid');
}

validatePassoword('logIn');
