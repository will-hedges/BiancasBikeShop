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
  return fetch(`${apiUrl}/${id}`, { method: "GET" }).then((res) => {
    if (res.ok) {
      return res.json();
    } else {
      throw new Error(
        "An unknown error occurred while trying to get the bike."
      );
    }
  });
};

export const getBikesInShopCount = () => {
  return fetch(`${apiUrl}/inshop`, { method: "GET" }).then((res) => {
    if (res.ok) {
      return res.json();
    } else {
      throw new Error(
        "An unknown error occurred while trying to count bikes in the shop."
      );
    }
  });
};
