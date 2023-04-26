const apiUrl = "/api/bike";

export const getBikes = () => {
  return fetch(apiUrl, { method: "GET" }).then((res) => {
    if (res.ok) {
      return res.json();
    } else {
      throw new Error("An unknown error occurred while trying to get bikes");
    }
  });
};

export const getBikeById = (id) => {
  //add implementation here...
};

export const getBikesInShopCount = () => {
  //add implementation here...
};
