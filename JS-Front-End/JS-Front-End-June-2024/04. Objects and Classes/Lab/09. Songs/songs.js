function printSongsByType(inputArray) {
  class Song {
    constructor(typeList, name, time) {
      this.typeList = typeList;
      this.name = name;
      this.time = time;
    }
  }
  const desiredTypeList = inputArray[inputArray.length - 1];
  const songEntries = inputArray.slice(1, -1);

  const allSongs = [];

  songEntries.forEach((entry) => {
    const [typeList, name, time] = entry.split('_');

    const song = new Song(typeList, name, time);
    allSongs.push(song);
  });

  if (desiredTypeList === 'all') {
    allSongs.forEach((song) => {
      console.log(`${song.name}`);
    });
  } else {
    const filteredSongs = allSongs.filter(
      (song) => song.typeList === desiredTypeList
    );

    filteredSongs.forEach((song) => {
      console.log(`${song.name}`);
    });
  }
}

printSongsByType([
  3,
  'favourite_DownTown_3:14',
  'favourite_Kiss_4:16',
  'favourite_Smooth Criminal_4:01',
  'favourite',
]);
