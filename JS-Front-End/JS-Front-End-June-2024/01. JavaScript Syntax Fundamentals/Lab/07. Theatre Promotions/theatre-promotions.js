function solve(day, age) {
  let result = 'Error!';
  if (day === 'Weekday') {
    if ((age >= 0 && age <= 18) || (age > 64 && age <= 122)) {
      result = '12$';
    } else if (age > 18 && age <= 64) {
      result = '18$';
    }
  } else if (day === 'Weekend') {
    if ((age >= 0 && age <= 18) || (age > 64 && age <= 122)) {
      result = '15$';
    } else if (age > 18 && age <= 64) {
      result = '20$';
    }
  } else if (day === 'Holiday') {
    if (age >= 0 && age <= 18) {
      result = '5$';
    } else if (age > 18 && age <= 64) {
      result = '12$';
    } else if (age > 64 && age <= 122) {
      result = '10$';
    }
  }

  console.log(result);
}

solve('Holiday', 5);
