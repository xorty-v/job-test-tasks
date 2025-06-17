const refreshAuthTokens = async () => {
  const refreshToken = localStorage.getItem("refreshToken");
  if (!refreshToken) {
    console.warn("Нет refresh-токена для обновления.");
    return false;
  }

  try {
    const response = await fetch("/auth/refresh-token", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ refreshToken }),
    });

    if (response.ok) {
      const data = await response.json();
      localStorage.setItem("accessToken", data.accessToken);
      localStorage.setItem("refreshToken", data.refreshToken);
      return true;
    } else {
      console.error("Не удалось обновить токены:", response.statusText);
      localStorage.removeItem("accessToken");
      localStorage.removeItem("refreshToken");
      return false;
    }
  } catch (error) {
    console.error("Ошибка при обновлении токена:", error);
    localStorage.removeItem("accessToken");
    localStorage.removeItem("refreshToken");
    return false;
  }
};

export const fetchWithAuth = async (url, options = {}, handleUnauthorized) => {
  let accessToken = localStorage.getItem("accessToken");
  let headers = {
    "Content-Type": "application/json",
    ...options.headers,
  };

  let response = await fetchWithToken(url, accessToken, headers, options);

  if (response.status === 401 && !url.includes("/auth/login")) {
    const isRefreshed = await refreshAuthTokens();

    if (isRefreshed) {
      accessToken = localStorage.getItem("accessToken");
      response = await fetchWithToken(url, accessToken, headers, options);
    } else {
      handleUnauthorized();
      return new Response(JSON.stringify({ message: "Сессия истекла" }), {
        status: 401,
        headers: { "Content-Type": "application/json" },
      });
    }
  }

  return response;
};

const fetchWithToken = (url, accessToken, headers, options) => {
  const authHeaders = accessToken
    ? { ...headers, Authorization: `Bearer ${accessToken}` }
    : headers;

  return fetch(url, {
    ...options,
    headers: authHeaders,
  });
};
