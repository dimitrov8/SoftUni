function formatGrade(gradeInput) {
  if (gradeInput < 3.0) {
    console.log('Fail (2)');
    return;
  } else if (gradeInput >= 3.0 && gradeInput < 3.5) {
    output = 'Poor';
  } else if (gradeInput >= 3.5 && gradeInput < 4.5) {
    output = 'Good';
  } else if (gradeInput >= 4.5 && gradeInput < 5.5) {
    output = 'Very good';
  } else if (gradeInput >= 5.5) {
    output = 'Excellent';
  }

  output += ' ' + `(${gradeInput.toFixed(2)})`;

  console.log(output);
}

formatGrade(3.33);
