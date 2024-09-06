export const loadTechnologies = async (/* uid: string */) => {
  // const technologies: Technology[] = [];
  const res = await fetch("https://juniorhub.somee.com/api/technologies");
  const { data } = await res.json();
  return data;

  // if (!uid) throw new Error("UID is undefined");
  // const collectionRef = collection(FirebaseDB, `${uid}/journal/notes`);
  // const docs = await getDocs(collectionRef);

  // const notes: Offer[] = [];
  // docs.forEach((doc) => {
  //   notes.push({
  //     id: doc.id,
  //     ...(doc.data() as Offer),
  //   });
  // });
};
