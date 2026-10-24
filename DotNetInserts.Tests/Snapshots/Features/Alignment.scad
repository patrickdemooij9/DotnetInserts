$fn=48;
translate([0,0,0])
{
difference()
{
    cube(size = [60.00, 40.00, 20.00], center = false); 
    translate(v = [3.00, 10.00, 4.00])
    {
        cube(size = [20.00, 20.00, 16.00], center = false); 
    }
    translate(v = [25.00, 10.00, 4.00])
    {
        cube(size = [14.00, 20.00, 16.00], center = false); 
    }
}

}
translate([65,0,0])
{
cube(size = [40.00, 40.00, 20.00], center = false); 

}
translate([65,44,0])
{
cube(size = [30.00, 30.00, 20.00], center = false); 

}
