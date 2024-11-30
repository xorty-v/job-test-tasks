CREATE TABLE IF NOT EXISTS "User" (
    "Id" BIGSERIAL PRIMARY KEY,
    "Name" VARCHAR(255) NOT NULL,
    "Balance" DOUBLE PRECISION NOT NULL
);

CREATE TABLE IF NOT EXISTS "MatchHistory" (
    "Id" BIGSERIAL PRIMARY KEY,
    "FirstUserId" BIGINT,
    "SecondUserId" BIGINT,
    "Stake" DOUBLE PRECISION NOT NULL,
    "FirstUserChoice" VARCHAR(255),
    "SecondUserChoice" VARCHAR(255),
    "WinnerId" BIGINT,
    FOREIGN KEY ("FirstUserId") REFERENCES "User"("Id"),
    FOREIGN KEY ("SecondUserId") REFERENCES "User"("Id"),
    FOREIGN KEY ("WinnerId") REFERENCES "User"("Id")
);

CREATE TABLE IF NOT EXISTS "GameTransaction" (
    "Id" BIGSERIAL PRIMARY KEY,
    "SenderId" BIGINT NOT NULL,
    "ReceiverId" BIGINT NOT NULL,
    "Amount" DOUBLE PRECISION NOT NULL,
    FOREIGN KEY ("SenderId") REFERENCES "User"("Id"),
    FOREIGN KEY ("ReceiverId") REFERENCES "User"("Id")
);