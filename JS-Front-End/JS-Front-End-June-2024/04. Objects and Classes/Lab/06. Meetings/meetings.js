function manageMeetings(inputArray) {
  const meetings = {};

  inputArray.forEach((entry) => {
    const [day, name] = entry.split(' ');

    if (meetings[day]) {
      console.log(`Conflict on ${day}!`);
    } else {
      meetings[day] = name;
      console.log(`Scheduled for ${day}`);
    }
  });

  for (let key in meetings) {
    console.log(`${key} -> ${meetings[key]}`);
  }
}

manageMeetings(['Monday Peter', 'Wednesday Bill', 'Monday Tim', 'Friday Tim']);
