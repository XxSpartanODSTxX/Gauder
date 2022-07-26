/*Main Test Program
 * Version 1.0 Beta vom 2.06.2021
 */
void mainTestProgram() {
  float axis1 = getDrivePosition(0x601);
  float axis2 = getDrivePosition(0x602);
  resetState(POSITION_InBit);
  Motion.reset(axis1, axis2);
  Motion.moveOut();
  Motion.movebSpline();
  setState(POSITION_OutBit);
  Motion.moveIn();
  Motion.movebSpline();
  resetState(POSITION_OutBit);
  setState(POSITION_InBit);

  float* step;
  int index = 0; //float xx; float xy; float dx1, dy1;
  while ((step = Motion.getNextStep(index++)) != NULL) {
    xx = step[0];
    xy = step[1];
    dx1 = step[2];
    dy1 = step[3];
    bColl = Collision.checkColl_RLE(step[0], step[1]);
    bColl = Collision.checkCollisionQuadTree(step[0], step[1]);
    bColl = Collision.checkCollissionGeom(step[0], step[1]);
    bColl = Collision.check(step[0], step[1]);
    posicionMotor1 = -1 * step[0] * gear1 * 2000 / M_PI;
    posicionMotor2 = -1 * step[1] * gear2 * 2000 / M_PI;
    getMotorData(xx, xy, dx1, dy1);
    canOpen.setInterPosition(posicionMotor1, 0x601);
    delay(10);
    canOpen.setInterPosition(posicionMotor2, 0x602);
    delay(10);
  }
}
