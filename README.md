# MXEN3000 — Line Following Robot GUI

A C# Windows Forms application developed for the MXEN3000 Design Project at Curtin University. The GUI communicates with an Arduino-Nano-based line-following robot over serial, providing real-time sensor feedback, live PID tuning, and multiple control modes selectable at runtime.

---

## Features

- **Multiple control modes** — Manual, PID, Bang-Bang, Proportional, and PI, cycled via a single toggle button
- **Live PID tuning** — Kp, Ki, and Kd values are editable in the GUI and take effect immediately without recompiling
- **Auto-calibration** — Samples both sensors over ~6 seconds and automatically sets min/max bounds for normalised error calculation
- **Real-time sensor display** — Left and right sensor values shown continuously and mapped to a normalised `[-1, 1]` error signal
- **Integral reset** — One-click integral wind-up reset during a run
- **Serial packet protocol** — 4-byte framed messages with checksum verification between the GUI and Arduino IO card

---

## Control Modes

Modes are cycled using the **Toggle Mode** button. The button label updates to reflect the current mode.

| Mode | Description |
|------|-------------|
| **Manual** | PWM value sent directly via Output 1 / Output 2 buttons |
| **PID** | Full PID controller using mapped sensor error. Gains editable live |
| **Bang-Bang** | Alternating full-correction pulses — cranks one motor hard for a set delay, then returns to base speed |
| **Proportional** | Proportional-only control (implementation stub) |
| **PI** | Proportional + Integral control (implementation stub) |

---

## PID Controller

The error signal is derived from normalised left and right sensor readings:

```
error = leftMap - rightMap

leftMap  = (lMax - leftSensor)  / (lMax - lMin)
rightMap = (rMax - rightSensor) / (rMax - rMin)
```

The PID output is then applied symmetrically to each motor:

```
leftSpeed  = baseSpeed - 0.8 * PID
rightSpeed = baseSpeed + 0.8 * PID
```

Default tuning values:

| Parameter | Default | Description |
|-----------|---------|-------------|
| `kp` | `33` | Proportional gain |
| `ki` | `0.007` | Integral gain |
| `kd` | `0.04` | Derivative gain |
| `baseSpeed` | `170` | Base PWM sent to both motors |
| `maxIntegral` | `250` | Anti-windup clamp on integral term |
| `dt` | `20` ms | Control loop time step |

The derivative term is smoothed using a 5-sample rolling average to reduce noise sensitivity.

---

## Serial Protocol

Communication uses a fixed 4-byte framed packet with a checksum. The Arduino IO card driver handles the same format on the embedded side.

### Packet Format

| Byte | Field | Description |
|------|-------|-------------|
| 0 | `START` (255) | Frame delimiter |
| 1 | `PORT` | `0` = Input 1, `1` = Input 2, `2` = Output 1, `3` = Output 2 |
| 2 | `DATA` | Value to write (outputs) or `0` for read requests (inputs) |
| 3 | Checksum | `(START + PORT + DATA) % 256` |

Sensor reads are requested every control loop tick. The GUI reads the serial buffer each timer tick and validates the checksum before accepting data.

---

## Calibration

Click **Recalibrate** to start an automatic calibration sequence:

1. The cart should be moved across the full sensor range (line edge to edge) during the ~6 second window.
2. The GUI samples both sensors and records min/max values with a ±2 margin.
3. Values are written back to `lMin`, `lMax`, `rMin`, `rMax` and displayed in the GUI.
4. The integral is reset automatically at the end of calibration.

Default sensor bounds (used before calibration):

| Bound | Default |
|-------|---------|
| `lMin` | 155 |
| `lMax` | 235 |
| `rMin` | 130 |
| `rMax` | 230 |

---

## Bang-Bang Parameters

| Parameter | Default | Description |
|-----------|---------|-------------|
| `freakyThang` | `0.6` | Error threshold to trigger a correction pulse |
| `freakyBasespeed` | `100` | Base PWM during Bang-Bang mode |
| `freakyAdjustment` | `0` | Subtracted from max PWM during correction pulse |
| `freakyDelay` | `300` ms | Duration of each correction pulse |
