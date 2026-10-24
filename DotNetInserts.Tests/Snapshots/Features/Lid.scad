$fn=48;
translate([0,0,0])
{
difference()
{
    translate(v = [1.50, 1.50, 0.00])
    {
        hull()
        {
            cylinder(center = false, r1 = 1.5, r2 = 1.5, h = 35);
            translate(v = [77.00, 0.00, 0.00])
            {
                cylinder(center = false, r1 = 1.5, r2 = 1.5, h = 35, $fn = 50);
            }
            translate(v = [0.00, 57.00, 0.00])
            {
                cylinder(center = false, r1 = 1.5, r2 = 1.5, h = 35, $fn = 50);
            }
            translate(v = [77.00, 57.00, 0.00])
            {
                cylinder(center = false, r1 = 1.5, r2 = 1.5, h = 35, $fn = 50);
            }
        }
    }
    translate(v = [0.00, 0.00, 28.00])
    {
        difference()
        {
            cube(size = [80.00, 60.00, 7.00], center = false); 
            translate(v = [0.80, 0.80, 0.00])
            {
                cube(size = [78.40, 58.40, 5.00], center = false); 
            }
            translate(v = [30.80, 0.00, 0.00])
            {
                cube(size = [18.40, 60.00, 1.00], center = false); 
            }
            translate(v = [0.00, 30.80, 0.00])
            {
                cube(size = [80.00, -1.60, 1.00], center = false); 
            }
        }
    }
    translate(v = [3.00, 3.00, 5.00])
    {
        cube(size = [74.00, 54.00, 28.00], center = false); 
    }
}

}
