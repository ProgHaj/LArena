cp InputTemplateBase InputTemplateBase.temp
for i in `seq 1 16`;
do
    cp InputTemplate InputTemplate.temp
    sed -i -- "s/TEMP/$i/g" InputTemplate.temp
    cat InputTemplate.temp >> InputTemplateBase.temp
    rm InputTemplate.temp
done   

cp InputTemplate InputTemplate.temp
sed -i -- "s/joyNum: TEMP/joyNum: 0/g" InputTemplate.temp
sed -i -- "s/joystick TEMP button/joystick button/g" InputTemplate.temp
sed -i -- "s/TEMP//g" InputTemplate.temp
head -n -64 InputTemplate.temp > InputTemplate.temp2
cat InputTemplate.temp2 >> InputTemplateBase.temp
rm InputTemplate.temp
rm InputTemplate.temp2
mv InputTemplateBase.temp InputManager.asset
