function manageParkingLot(inputArray) {
  const parkingLot = [];

  inputArray.forEach((entry) => {
    const [direction, carNumber] = entry.split(', ');

    if (direction === 'IN') {
      // Add car number only if it's not already present
      if (!parkingLot.includes(carNumber)) {
        parkingLot.push(carNumber);
      }
    } else if (direction === 'OUT') {
      // Remove car number if it exists in the array
      const index = parkingLot.indexOf(carNumber);

      if (index !== -1) {
        parkingLot.splice(index, 1);
      }
    }
  });

  if (parkingLot.length === 0) {
    console.log('Parking Lot is Empty');
  } else {
    // Sort the array and print each car number
    parkingLot.sort();
    parkingLot.forEach((car) => console.log(car));
  }
}

manageParkingLot([
  'IN, CA2844AA',
  'IN, CA1234TA',
  'OUT, CA2844AA',
  'IN, CA9999TT',
  'IN, CA2866HI',
  'OUT, CA1234TA',
  'IN, CA2844AA',
  'OUT, CA2866HI',
  'IN, CA9876HH',
  'IN, CA2822UU',
]);
