function displayTownInfo(inputArray) {
  const allTowns = [];

  inputArray.forEach((row) => {
    const [name, latitude, longitude] = row.split(' | ').map((x) => x.trim());
    allTowns.push({
      town: name,
      latitude: parseFloat(latitude).toFixed(2),
      longitude: parseFloat(longitude).toFixed(2),
    });
  });

  allTowns.forEach((town) => {
    console.log(town);
  });
}

displayTownInfo([
  'Sofia | 42.696552 | 23.32601',
  'Beijing | 39.913818 | 116.363625',
]);
