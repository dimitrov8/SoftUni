function loadingBar(percentage) {
  const totalSegments = 10;
  const filledSegments = Math.round((percentage / 100) * totalSegments);

  if (filledSegments === totalSegments) {
    console.log('100% Complete!');
    return;
  }

  const emptySegments = totalSegments - filledSegments;

  const filledBar = '%'.repeat(filledSegments);
  const emptyBar = '.'.repeat(emptySegments);

  const loadingBarString = filledBar + emptyBar;

  console.log(`${percentage}% [${loadingBarString}]`);
  console.log('Still loading...');
}

loadingBar(30);
