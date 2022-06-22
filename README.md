# powerplant-coding-challenge

## Local (Windows - VS22)
git clone \<repo\>  
open .sln file with VS22  
build & start solution

## Docker
docker build . -t \<tag\> (run this in ./PowerCodingChallenge where the Dockerfile is located)  
docker run -p 8888:80 \<tag\>


# Endpoints
localhost:8888/productionplan  
with appropriate payload as body