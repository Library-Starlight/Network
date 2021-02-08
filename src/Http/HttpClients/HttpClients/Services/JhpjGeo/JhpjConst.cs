using System;
using System.Collections.Generic;
using System.Text;

namespace HttpClients.Services.JhpjGeo
{
    public class JhpjConst
    {
        public const string HelloWorldRoute = "/gateway/helloworld";

        public const string StatusChangeRoute = "/gateway/statuschange";

        public const string StatusRoute = "/gateway/status";

        public const string AppId = "df6b4e45b26343fbb60216af2182b4eb";

        public const string PublicKey = @"MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA7fu8p4ANLA1U2B4u0boF
3JJOE8BYs8TrApnN/pu4QJoo0DwWmq+aoNJFAN/tssccsXWGdT9n0dbbuVoMVrqQ
2/mqS6oZq0aPayYM8ajhJVS70Hgr1fPMkBfUHuSN75r6bvIMQX+jVxu8dvtRnXbp
9loJSiISNd2d4dh4xd+9go4atyDPQmE829Hk7CFf3jN7Mp4OPBq0AT2YTUKwjf7W
HCcVT/XAK0n4vNX0XWv4Dd8a2EUMzmG+byOycW/l6tOPYqERixnkPTnn4J5GGm7U
BTXxjRJWdWS7o2YqLPrP9SVMWQnCxbJxTzUX0PErUgCG202/f8Y9arSktndzygRx
MQIDAQAB";

        public const string PrivateKey = @"MIIEogIBAAKCAQEA7fu8p4ANLA1U2B4u0boF3JJOE8BYs8TrApnN/pu4QJoo0DwW
mq+aoNJFAN/tssccsXWGdT9n0dbbuVoMVrqQ2/mqS6oZq0aPayYM8ajhJVS70Hgr
1fPMkBfUHuSN75r6bvIMQX+jVxu8dvtRnXbp9loJSiISNd2d4dh4xd+9go4atyDP
QmE829Hk7CFf3jN7Mp4OPBq0AT2YTUKwjf7WHCcVT/XAK0n4vNX0XWv4Dd8a2EUM
zmG+byOycW/l6tOPYqERixnkPTnn4J5GGm7UBTXxjRJWdWS7o2YqLPrP9SVMWQnC
xbJxTzUX0PErUgCG202/f8Y9arSktndzygRxMQIDAQABAoIBAH2XbnYPE+R9nJr5
QsgXneRLqbyus6EbeNLEjHujfldJg+Rw8ZTsu3PHw/a0NmM0xSW52h2iOo35D7f/
l6dlifEtDvwjZqyG7kUjrY0TDM6AqyVXZfrhUMFycdVj1KvwY92NC369d1wPHSTF
eMra6JnD5kcjN+0Jabq4XPRAIWj1xQiIwFbQuefBN/itT1cdJaauWYwMsmGqovHn
z/hy25gFKKdZ/NNPBuvQ09+dJne7y28dqrqiebkh/iH0My2/zrbW8/kjU51RbxeH
mUAO5m2sUPlzx1OYtBVGQaFt+kxFbGJwn4MAUDwNZoUMGmE2u3y7enlfzt1Slwg6
lJYplGECgYEA+HeH0GPHs9MJcVSJvnroJYUhQYjJwUw7H4R8pq9M2Bg3ej6UL+P9
dItqV781o6kP2rFioKO91p8b/HoAMXV+nO3AypHZ/POcydf9/meeS7/TsuQvmT7S
q+wY4Bad3ScZgEzkBFIi8VJXVho/yrbrXuuSKaoCvsEstzgIMcPWgv0CgYEA9TLW
pXz2K98HQDHOusXl4SEtoLsG8Pjr4Hrixf48XKYXAhU5rmEtQ5z4j1X6Pa2r3HUK
K+qXM4lM3T24xHntsCGbnJy7WIQf+pJehDBZQWbOEejvhhL3rRvgJmXkLpwhURbQ
PP77nesHz0kJlPixX8ASk9wcwlngXLzKYiC4n0UCgYB2mvhl7XuCx5aWVrh6CMmQ
P4Cke9tzPiRq4x9tofiYnta8r+qlAbwXSgW1ZT0Bps4+orZl4EPak5EUcFhvQ5rw
5j1FdWINcWzCnSxNqMDTf/paRrrB+E8waj9lmOY6j1OI/ytJNLwhXiD6pQUWi0LH
yMO4x8MPs77YpxKiTs2zMQKBgB37v3+xDjFDLpHNBWiVdozXD8tN04V4CvACVuS7
OApkTP/2NH18aJqSAVsVumn8aL2mmj3Qi/jDW2DagVOVTVlyYqL+D3K9hWOQrnof
p2p19dD+2PteKmdCt2A6qXKBqRlPXtt2mrIu4V+ZoNBUsOvygtMN4X5qmyL7+wuQ
0MK5AoGAU0q2y0ehmpec0uFpsIAh0GP+zDPR53Q1Fcyau+2YGS4IvRFYVZerY3A0
a426csmAP/p5HJR0hGWlmReUeJ7ecUoJFkDLDKza4DHJcy/3C79joXXFaWUxt2j1
7qNZX+iLACxdAJwBThbMeCajW0TrlaH/bOTa3pSXO7SIs5HQRPo=";

        public const string EncryptKey = "d9b46c3513654f66bea91f7e81009ce9";

        public static string GetRoute(string hostUrl, string route)
            => $"{hostUrl}{route}";
    }
}
